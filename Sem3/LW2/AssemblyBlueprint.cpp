#include "AssemblyBlueprint.h"

void AssemblyBlueprint::show() const {
	showHeader();
	showBlueprintHeader();

	for (const auto e : elements)
		e->showContent();

	showReferences();
}
