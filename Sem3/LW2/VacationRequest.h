#pragma once

#include <string>
#include <iostream>
#include "Request.h"

using std::string;
using std::pair;

class VacationRequest final : public Request {
private:
	pair<Date, Date> vacationDates;

public:
	VacationRequest();
	VacationRequest(const string& title, pair<Date, Date> vacationDates);
	void show() const override;
};