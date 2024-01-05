
#pragma once

#include <sc-memory/sc_addr.hpp>
#include <sc-memory/sc_object.hpp>

#include "keynodes.generated.hpp"

namespace iSearchModuleNamespace
{
class Keynodes : public ScObject
{
  SC_CLASS()
  SC_GENERATED_BODY()

public:
  SC_PROPERTY(Keynode("question_isearch"), ForceCreate)
  static ScAddr question_isearch;

  SC_PROPERTY(Keynode("rrel_isearch_pattern"), ForceCreate)
  static ScAddr rrel_isearch_pattern;

  SC_PROPERTY(Keynode("rrel_isearch_source"), ForceCreate)
  static ScAddr rrel_isearch_source;

  SC_PROPERTY(Keynode("nrel_search_result"), ForceCreate)
  static ScAddr nrel_search_result;
};

}