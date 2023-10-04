#pragma once

#include <string>
#include <iostream>
#include <ctime>

class Date {
private:
	uint8_t day = 1;
	uint8_t month = 1;
	uint32_t year = 2023;

public:

	Date(uint8_t day, uint8_t month, uint32_t year);

	Date();

	std::string toString() const;

	bool operator<(const Date& other) const;

};
