#pragma once

#include <string>
#include <iostream>
#include "Application.h"

using std::string;
using std::pair;

class VacationApplication : public Application {
private:
	pair<Date, Date> vacationDates;

public:
	VacationApplication();
	VacationApplication(const string& title, pair<Date, Date> vacationDates);
	void show() const override;
};