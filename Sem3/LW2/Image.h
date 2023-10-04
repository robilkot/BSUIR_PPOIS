#pragma once

#include <iostream>
#include <string>

using std::string;
using std::cout;

class Image {
	string filePath;
	bool isEmpty;

public:
	string description;

	Image();
	Image(const string& filePath, const string& description);
	
	void setPath(const string& filePath);
	bool empty() const;
	void show() const;
};