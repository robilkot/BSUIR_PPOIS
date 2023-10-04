#pragma once

#include "Blueprint.h"
#include <iostream>
#include <string>
#include <set>

using std::string;
using std::cout;

enum class ModelRenderer {
	VRay,
	Corona,
	RayTracing
};

class ThreeDimensionalBlueprint : public Blueprint {
private:
	double origin[3] = { 0,0,0 };
	string modelPath;
	string materialsPath;

	ModelRenderer renderer = ModelRenderer::VRay;

	void renderPreview(ModelRenderer renderer) const;

public:
	void show() const override;
};

