import hashlib
import os

duplicate_options = ["size", "hash"]

def find_duplicate_files(file_list, include_single_file):
    file_list_len = len(file_list)
    if file_list_len < 1:
        return []

    devide_lists = [range(file_list_len)]

    def devide_files(file_index_list, get_compare_function):
        result = []
        file_index_list_copy = list(file_index_list)
        compare = get_compare_function(file_index_list)
        while len(file_index_list_copy) > 0:
            current_list = []
            first = file_index_list_copy.pop(0)
            current_list.append(first)
            i = 0
            while i < len(file_index_list_copy):
                if compare(first, file_index_list_copy[i]):
                    current_list.append(file_index_list_copy.pop(i))
                else:
                    i += 1
            if include_single_file or len(current_list) > 1:
                result.append(current_list)
        return result

    # Compare size method.
    get_compare_function_lib = {}
    def register_compare_function(name, get_compare_function):
        get_compare_function_lib[name] = get_compare_function

    def get_compare_size_function(file_index_list):
        size_list = {i: os.path.getsize(file_list[i]) for i in file_index_list}
        def compare_size(x, y):
            return size_list[x] == size_list[y]
        return compare_size
    register_compare_function("size", get_compare_size_function)

    # Compare hash method.
    def get_compare_hash_function(file_index_list):
        def get_sha1(path):
            f = open(path, "rb")
            h = hashlib.sha1()
            while True:
                data = f.read(65536)
                if not data:
                    break
                h.update(data)
            return h.digest()
        hash_dict = {i: get_sha1(file_list[i]) for i in file_index_list}
        def compare_hash(x, y):
            return hash_dict[x] == hash_dict[y]
        return compare_hash
    register_compare_function("hash", get_compare_hash_function)


    get_compare_function_list = []
    for option in duplicate_options:
        get_compare_function_list.append(get_compare_function_lib[option])

    for get_compare_function in get_compare_function_list:
        new_devide_lists = []
        for devide_list in devide_lists:
            new_devide_lists.extend(devide_files(devide_list, get_compare_function))
        devide_lists = new_devide_lists

    return devide_lists
