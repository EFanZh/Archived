#!/usr/bin/env python

source = r"""#!/usr/bin/env python

source = r{}

print(source.format('"' * 3 + source + '"' * 3))"""

print(source.format('"' * 3 + source + '"' * 3))
