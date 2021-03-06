# -*- coding: utf-8 -*-
# 该工具用于将excel文件传为json文件
# 命令格式：python xml_generate.py excel.xlsx
#         python xml_generate.py ../directory/excel.xls
#         python xml_generate.py directory/excel.xls

from base import *
from types import *
import json

WRITE_DIRECTORY = 'localization'
CONFIG_TABLE_NAME = '_config_'

class LocalizationGenerate(Base):

    _output_json = {}

    def __init__(self):
        Base.__init__(self)
        self._make_dirs(WRITE_DIRECTORY)

    def load_xls_file(self, xls_file):
        self._load_xls_file(xls_file)
        self._output_json = {}

    def table_to_json(self, sheet_name, output_name):
        table = self._try_get_sheet_by_name(sheet_name)
        column_names = table.row_values(TABLE_COLUMN_NAME_ROW_INDEX)
        column_types = table.row_values(TABLE_COLUMN_TYPE_ROW_INDEX)

        table_json = {}
        for column_index in range(table.ncols - 1):
            column_name = column_names[column_index + 1]
            column_type = column_types[column_index + 1]
            if column_type == COLUMN_MACRO_TYPE:
                continue
            if len(column_name) == '' or str(column_name).strip() == '':
                continue

            subtable_json = {}
            for row_index in range(TABLE_OFFSET_ROW_NUM, table.nrows):
                cell = table.cell(row_index, 0)
                if cell.value == '' or str(cell.value).strip() == '':
                    continue

                languageid = int(cell.value)

                cell = table.cell(row_index, column_index + 1)
                value = str(cell.value).strip()
                if len(value) > 0:
                    subtable_json[str(languageid)] = value
            table_json[column_name] = subtable_json
        
        return table_json

    def parse_excel(self):
        self._output_json = {}
        for row in self._output_config_table:
            # 输出的文件名
            output_file_name = row[CONFIG_OUTPUT_FILE_NAME_COLUMN_INDEX]

            # 输出的sheet名
            output_sheet_name = row[CONFIG_SHEET_NAME_COLUMN_INDEX]

            # 表的字段名
            output_table_name = row[CONFIG_OUTPUT_NAME_COLUMN_INDEX]

            output_json = self.table_to_json(output_sheet_name, output_table_name)
            for key in output_json:
                if self._output_json.has_key(key):
                    self._output_json[key].update(output_json[key])
                else:
                    self._output_json[key] = output_json[key]

    def export_json(self):
        for key in self._output_json.keys():
            value = self._output_json.get(key)
            if value != None:
                self.write_json(key, json.dumps(value, indent=4, sort_keys=True, ensure_ascii=False, separators=(',',':')))

    def write_json(self, file_name, text):
        file_name += '.json'
        self._write(WRITE_DIRECTORY, file_name, text)
