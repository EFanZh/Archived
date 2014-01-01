import hashlib
import os
from find_duplicate_files import *
from find_all_files import *

# Tests.
find_duplicate_files(list(find_all_files(".")), False)
