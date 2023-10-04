#include "ThreeDimensionalBlueprint.h"

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