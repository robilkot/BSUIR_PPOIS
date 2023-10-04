#pragma once

#include <iostream>
#include <string>

class Person {
public:
	std::string name;
	std::string surname;
	std::string position;

	Person();
	Person(const std::string& name, const std::string& surname, const std::string& position);
	std::string toString() const;
	bool operator<(const Person& other) const;
};