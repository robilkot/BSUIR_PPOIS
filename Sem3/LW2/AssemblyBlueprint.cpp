#include "AssemblyBlueprint.h"

void AssemblyBlueprint::addItem(Blueprint* item) {
	elements.emplace(item);
	updateEditDate();
}

void AssemblyBlueprint::removeItem(Blueprint* item) {
	elements.erase(item);
	updateEditDate();
}

void AssemblyBlueprint::show() const {
	showHeader();
	showBlueprintHeader();

	for (const auto e : elements)
		e->showContent();

	showReferences();
}
