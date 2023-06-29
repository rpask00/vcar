import os
import glob

def add_empty_line_to_file(file_path):
    with open(file_path, 'a') as file:
        file.write('\n')

def process_directory(directory_path):
    # Przechodzi przez wszystkie pliki w danym katalogu
    for filepath in glob.glob(directory_path + '/**', recursive=True):
        if os.path.isfile(filepath):
            add_empty_line_to_file(filepath)

# Wywołanie funkcji na konkretnym katalogu
process_directory('./')


