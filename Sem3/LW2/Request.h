#pragma once

#include <string>
#include <iostream>
#include "Document.h"

using std::string;
using std::cout;

class Request : public Document {
protected:
	string request;

public:
	Request();
	Request(const string& title, const string& request);

	void show() const override;
};