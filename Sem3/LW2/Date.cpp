#include "Date.h"

Date::Date(uint8_t day, uint8_t month, uint32_t year = 2023)
		: day(day), month(month), year(year) {};

Date::Date() {
	time_t t = std::time(0);
	auto today = std::localtime(&t);
	year = today->tm_year + 1900;
	month = today->tm_mon + 1;
	day = today->tm_mday;
}

std::string Date::toString() const {
	std::string result;

	if (day < 10) result += '0';
	result += std::to_string(day);
	result += '.';

	if (month < 10) result += '0';
	result += std::to_string(month);
	result += '.';

	result += std::to_string(year);
	return result;
}

bool Date::operator < (const Date& other) const {
	if (this->year < other.year) return true;
	if (this->year > other.year) return false;

	if (this->month < other.month) return true;
	if (this->month > other.month) return false;

	if (this->day < other.day) return true;
	/*if (this->day >= other.day)*/ return false;
}
