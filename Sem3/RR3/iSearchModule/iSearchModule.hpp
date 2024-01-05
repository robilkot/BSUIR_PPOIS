
#pragma once

#include <sc-memory/sc_module.hpp>

#include "iSearchModule.generated.hpp"

class iSearchModule : public ScModule
{
  SC_CLASS(LoadOrder(50))
  SC_GENERATED_BODY()

  virtual sc_result InitializeImpl() override;

  virtual sc_result ShutdownImpl() override;
};
