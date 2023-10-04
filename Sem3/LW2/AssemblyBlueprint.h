#pragma once

#include "Blueprint.h"
#include <set>

class AssemblyBlueprint : public Blueprint {
protected:
	set<Blueprint*> elements;

public:
	void show() const override;
};