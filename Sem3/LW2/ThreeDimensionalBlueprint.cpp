#include "ThreeDimensionalBlueprint.h"

ThreeDimensionalBlueprint::ThreeDimensionalBlueprint() {}
ThreeDimensionalBlueprint::ThreeDimensionalBlueprint(const double const* origin, string modelPath = "Empty model", string materialsPath = "Empty materials")
	: modelPath(modelPath), materialsPath(materialsPath) {
	for(char i = 0; i < 3; i++)
		this->origin[i] = origin[i];
}

void ThreeDimensionalBlueprint::renderPreview(ModelRenderer renderer) const {
	switch (renderer) {
	case ModelRenderer::VRay: {
		cout << "--- Some cool render with VRay ---";
		break;
	}
	case ModelRenderer::Corona: {
		cout << "--- Some cool render with Corona ---";
		break;
	}
	case ModelRenderer::RayTracing: {
		cout << "--- Some cool render with RT ---";
		break;
	}
	default: {
		throw std::invalid_argument("No available renderer specified");
	}
	}
}

void ThreeDimensionalBlueprint::show() const {
	showHeader();
	showBlueprintHeader();

	cout << "Path to model: " << modelPath << '\n';
	cout << "Path to materials: " << materialsPath << '\n';

	cout << '\n';
	renderPreview(renderer);
	cout << '\n';

	showReferences();
}