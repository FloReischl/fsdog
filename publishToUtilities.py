import os, sys
import shutil
import msvcrt
 
# root_src_dir = r'C:\Users\n354651\source\repos\FsDog\FsDog\bin\Debug'
# root_dst_dir = r'C:\dev\tools\FsDog'
root_src_dir = r'C:\Users\flori\source\repos\FsDog\FsDog\bin\Debug'
root_dst_dir = r'C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog'
 
print("FsDog is about to get updated.\r\n")
input("Please close all instances now and press [Enter]...")

try:
    for src_dir, dirs, files in os.walk(root_src_dir):
        dst_dir = src_dir.replace(root_src_dir, root_dst_dir, 1)
        for file_ in files:
            src_file = os.path.join(src_dir, file_)
            dst_file = os.path.join(dst_dir, file_)
            if os.path.exists(dst_file):
                # in case of the src and dst are the same file
                if os.path.samefile(src_file, dst_file):
                    continue
                os.remove(dst_file)
            dest_file = os.path.join(dst_dir, file_)
            shutil.copy(src_file, dest_file)
            print(dest_file)
 
    print("\r\nSuccessfully updated. Start nowy? [y]")
    c = msvcrt.getch()
    if c.lower() == b'y':
        exe = os.path.join(root_dst_dir, 'FsDog.exe')
        os.system(f'START "" "{exe}"')
 
except BaseException as err:
    print(f"ERROR {err=}, {type(err)=}")
    input("\r\nPress any key to exit...")

