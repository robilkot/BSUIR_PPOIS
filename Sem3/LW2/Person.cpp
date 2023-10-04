#include "Person.h"

Person::Person() : name("Unknown"), surname("Unknown"), position("Unemployed") {};

Person::Person(const std::string& name, const std::string& surname, const std::string& position = "Unemployed") 
	: name(name), surname(surname), position(position) {} ;

std::string Person::toString() const {
	return name + ' ' + surname + ' ' + position;
}

bool Person::operator < (const Person& other) const {
	return this->name > other.name;
}
