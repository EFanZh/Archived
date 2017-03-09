#!/usr/bin/env python

source = r"""#!/usr/bin/env python

source = r{}

print(source.replace('{' + '}', '"' * 3 + source + '"' * 3))"""

print(source.replace('{' + '}', '"' * 3 + source + '"' * 3))
