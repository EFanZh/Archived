import os

def find_all_files(dir):
    for file in os.listdir(dir):
        abs_file = os.path.join(dir, file)
        if os.path.isfile(abs_file):
            yield abs_file;
        elif os.path.isdir(abs_file):
            for sub_file in find_all_files(abs_file):
                yield sub_file
