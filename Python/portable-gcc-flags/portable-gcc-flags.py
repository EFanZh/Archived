import argparse
import asyncio
import itertools
import re
import shlex
import subprocess
import sys

_GCC = ['gcc']
_CONCURRENCY = 4


async def _get_output(args):
    process = await asyncio.create_subprocess_exec(*args, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL)
    stdout, _stderr = await process.communicate()

    return stdout.decode()


def _get_gcc_command_line_options(arch, flags):
    switch_prefix = 'no-'
    switch_prefix_length = len(switch_prefix)

    def _helper():
        yield '-march={}'.format(arch)

        if flags:
            for key, value in sorted(flags, key=lambda item: item[0]):
                if isinstance(value, bool):
                    if value:
                        yield '-m{}'.format(key)
                    else:
                        if key.startswith(switch_prefix):
                            yield '-m{}'.format(key[switch_prefix_length:])
                        else:
                            yield '-m{}{}'.format(switch_prefix, key)
                else:
                    yield '-m{}={}'.format(key, value)

    return tuple(_helper())


def _get_command_line(args):
    return ' '.join(map(shlex.quote, args))


async def _get_raw_gcc_target_flags(arch, flags):
    command_line_options = _get_gcc_command_line_options(arch, flags)

    return await _get_output(args=[*_GCC, *command_line_options, '-Q', '--help=target'])


def _parse_gcc_target_flags(content):
    flag_line_regex = re.compile(r'\s*-m([^\s]+)\s*([^\s]*)\s*')

    for line in content.splitlines():
        match = flag_line_regex.fullmatch(line)

        try:
            key, value = match.groups()
        except AttributeError:
            continue

        if key.endswith('='):
            yield key[:-1], value
        else:
            yield key, (value == '[enabled]')


async def _get_gcc_target_flags(arch, flags=None):
    return tuple(_parse_gcc_target_flags(await _get_raw_gcc_target_flags(arch, flags)))


def _make_flag_key_extractor(arch):
    def _key(flags):
        return len(flags), len(_get_command_line(_get_gcc_command_line_options(arch, flags)))

    return _key


async def _simplify_gcc_target_flags(arch, flags):
    total_flags = len(flags)
    reference_result = await _get_gcc_target_flags(arch, flags)
    tested = 0
    total_tests = 2 ** len(flags)

    def _loop(index):
        if index < total_flags:
            for flags1 in _loop(index + 1):
                yield flags1
                yield (flags[index], *flags1)
        else:
            yield ()

    async def _test_flags(flags1):
        nonlocal tested

        result = await _get_gcc_target_flags(arch, flags1)

        tested += 1

        if tested % 32 == 0:
            print('Progress: {:.4} %'.format(tested * 100 / total_tests))

        return flags1, result == reference_result

    flags_iter = iter(_loop(0))

    result_futures = tuple(map(_test_flags, itertools.islice(flags_iter, _CONCURRENCY)))

    results = []

    while result_futures:
        done, pending = await asyncio.wait(result_futures, return_when=asyncio.FIRST_COMPLETED)

        pending.update(map(_test_flags, itertools.islice(flags_iter, _CONCURRENCY - len(pending))))

        result_futures = pending
        done_results = tuple(task.result() for task in done)

        results.extend(result for result, accept in done_results if accept)

    return min(results, key=_make_flag_key_extractor(arch))


async def _simplify_gcc_target_flags_fast(arch, flags):
    reference_result = await _get_gcc_target_flags(arch, flags)
    visited = set()
    concurrency = 0

    async def _test_flags(flags1):
        if flags1 not in visited:
            visited.add(flags1)

            nonlocal concurrency

            while concurrency >= _CONCURRENCY:
                await asyncio.sleep(0.1)

            concurrency += 1

            result = await _get_gcc_target_flags(arch, flags1)

            concurrency -= 1

            if result == reference_result:
                return await _simplify(flags1)
            else:
                return ()
        else:
            return ()

    def _set_remove(s, value):
        result = set(s)

        result.remove(value)

        return frozenset(result)

    async def _simplify(flags1):
        print('Processing: {}'.format(_get_command_line(_get_gcc_command_line_options(arch, flags1))))

        result_futures = []

        for flag in flags1:
            result_futures.append(_test_flags(_set_remove(flags1, flag)))

        results1 = tuple(itertools.chain.from_iterable(await asyncio.gather(*result_futures)))

        if results1:
            return results1
        else:
            return [flags1]

    results = await _simplify(frozenset(flags))

    return min(results, key=_make_flag_key_extractor(arch))


async def _main_async(arch, slow):
    native_flags = await _get_gcc_target_flags('native')
    arch = arch or next(value for key, value in native_flags if key == 'arch')
    arch_flags = set(await _get_gcc_target_flags(arch))

    significant_flags = tuple(flag for flag in native_flags if flag[0] != 'arch' and (flag not in arch_flags))

    if slow:
        simplified_flags = await _simplify_gcc_target_flags(arch, significant_flags)
    else:
        simplified_flags = await _simplify_gcc_target_flags_fast(arch, significant_flags)

    print('  Original flags: {}'.format(_get_command_line(_get_gcc_command_line_options(arch, significant_flags))))
    print('Simplified flags: {}'.format(_get_command_line(_get_gcc_command_line_options(arch, simplified_flags))))


def main():
    global _GCC
    global _CONCURRENCY

    arg_parser = argparse.ArgumentParser()

    arg_parser.add_argument('--arch', type=str)
    arg_parser.add_argument('--gcc', type=str, default='gcc')
    arg_parser.add_argument('--concurrency', type=int, default=4)
    arg_parser.add_argument('--slow', action='store_true')

    args = arg_parser.parse_args()

    _GCC = re.findall(r'[^\s]+', args.gcc)
    _CONCURRENCY = args.concurrency

    if sys.platform == 'win32':
        loop = asyncio.ProactorEventLoop()
    else:
        loop = asyncio.get_event_loop()

    try:
        loop.run_until_complete(_main_async(arch=args.arch, slow=args.slow))
    finally:
        loop.close()


if __name__ == '__main__':
    main()
