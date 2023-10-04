#pragma once

#include <string>
#include <iostream>
#include "Document.h"

using std::string;
using std::cout;

class Application : public Document {
protected:
	string request;

public:
	Application();
	Application(const string& title, const string& request);

	void show() const override;
};