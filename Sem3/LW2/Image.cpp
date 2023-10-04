#include <iostream>
#include <string>
#include "Image.h"

Image::Image()
	: description("No description"), isEmpty(true) {};

Image::Image(const string& filePath, const string& description = "No description")
	: filePath(filePath), description(description), isEmpty(false) {};

void Image::setPath(const string& filePath) {
	this->filePath = filePath;
	isEmpty = filePath.empty();
}

bool Image::empty() const {
	return isEmpty;
}
void Image::show() const {
	if (!description.empty()) {
		cout << "Image: " << description;
	}
	if (!filePath.empty())
		cout << "\nPath: " << filePath;
	else
		cout << "Image not found";
}

bool Image::operator < (const Image& other) const {
	return this->filePath > other.filePath;
}
bool Image::operator == (const Image& other) const {
	return this->filePath == other.filePath && this->description == other.description;
}
