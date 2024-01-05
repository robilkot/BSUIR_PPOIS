
#include "iSearchModule.hpp"
#include "keynodes/keynodes.hpp"
#include "agents/iSearchAgent.hpp"

using namespace iSearchModuleNamespace;

SC_IMPLEMENT_MODULE(iSearchModule)

sc_result iSearchModule::InitializeImpl()
{
  if (!Keynodes::InitGlobal())
    return SC_RESULT_ERROR;

  SC_AGENT_REGISTER(iSearchAgent)

  return SC_RESULT_OK;
}

sc_result iSearchModule::ShutdownImpl()
{
  SC_AGENT_UNREGISTER(iSearchAgent)

  return SC_RESULT_OK;
}
