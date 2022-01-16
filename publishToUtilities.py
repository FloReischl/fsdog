import os
import shutil

root_src_dir = r'C:\Users\flori\source\repos\FsDog\FsDog\bin\Debug'
root_dst_dir = r'C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog'

try:
    for src_dir, dirs, files in os.walk(root_src_dir):
        dst_dir = src_dir.replace(root_src_dir, root_dst_dir, 1)
        # if not os.path.exists(dst_dir):
        #     os.makedirs(dst_dir)
        for file_ in files:
            src_file = os.path.join(src_dir, file_)
            dst_file = os.path.join(dst_dir, file_)
            if os.path.exists(dst_file):
                # in case of the src and dst are the same file
                if os.path.samefile(src_file, dst_file):
                    continue
                os.remove(dst_file)
            shutil.move(src_file, dst_dir)
        print("done")
except BaseException as err:
    print(f"Unexpected {err=}, {type(err)=}")

input("press any key...")