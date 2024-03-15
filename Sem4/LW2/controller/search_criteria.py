from typing import Optional
from model.group import Group


class SearchCriteria:
    def __init__(self, group: Optional[Group], name: Optional[str], criteria: dict, page_number: int, page_size: int):
        self.group = group,
        self.name = name,
        self.criteria = criteria,
        self.page_number = page_number,
        self.page_size = page_size
