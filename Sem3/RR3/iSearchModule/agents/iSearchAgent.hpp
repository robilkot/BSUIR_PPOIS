
#pragma once

#include <sc-memory/kpm/sc_agent.hpp>

#include "keynodes/keynodes.hpp"
#include "iSearchAgent.generated.hpp"

namespace iSearchModuleNamespace
{

class iSearchAgent : public ScAgent
{
  SC_CLASS(Agent, Event(Keynodes::question_isearch, ScEvent::Type::AddOutputEdge))
  SC_GENERATED_BODY()

  void clearPreviousSearchResults(ScAddr const &);
  bool areAdjacent(const ScAddr&, const ScAddr&);
};

}
