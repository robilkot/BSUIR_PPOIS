#pragma once

#include <string>
#include <iostream>
#include "Document.h"

using std::string;
using std::cout;

class Report : public Document {
private:
	string subject;
	string institution;
	string text;

public:
	Report();
	Report(const string& title, const string& subject, const string& institution, const string& text);
	void show() const override;
};