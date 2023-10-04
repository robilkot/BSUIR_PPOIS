#pragma once

#include "Blueprint.h"
#include <set>

class AssemblyBlueprint : public Blueprint {
protected:
	set<Blueprint*> elements;

public:
	void addItem(Blueprint* item);
	void removeItem(Blueprint* item);

	void show() const override;
};