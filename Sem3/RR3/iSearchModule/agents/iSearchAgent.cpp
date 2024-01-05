#include <unordered_set>
#include <vector>

#include <sc-memory/sc_memory.hpp>

#include <sc-agents-common/utils/AgentUtils.hpp>

#include "iSearchAgent.hpp"

using namespace std;
using namespace utils;

using Isomorphism = unordered_map<const ScAddr, const ScAddr, ScAddrHashFunc<uint32_t>>; 

namespace iSearchModuleNamespace
{

SC_AGENT_IMPLEMENTATION(iSearchAgent)
{
  ScAddr actionNode = otherAddr;
  ScAddrVector answerElements;

  ScAddr pattern;
  ScAddr source;

  // Set pattern graph to last found in input structure
  ScIterator5Ptr patternIt = m_memoryCtx.Iterator5(
    actionNode,
    ScType::EdgeAccessConstPosPerm,
    ScType::NodeConst,
    ScType::EdgeAccessConstPosPerm,
    Keynodes::rrel_isearch_pattern
  );

  if(patternIt->Next())
    pattern = patternIt->Get(2);
  else
  {
    SC_LOG_ERROR("iSearch: Pattern graph not specified");
    utils::AgentUtils::finishAgentWork(&m_memoryCtx, actionNode, false);
    return SC_RESULT_ERROR_INVALID_PARAMS;
  }

  // Set source graph to last found in input structure
  ScIterator5Ptr sourceIt = m_memoryCtx.Iterator5(
    actionNode,
    ScType::EdgeAccessConstPosPerm,
    ScType::NodeConst,
    ScType::EdgeAccessConstPosPerm,
    Keynodes::rrel_isearch_source
  );

  if(sourceIt->Next())
    source = sourceIt->Get(2);
  else
  {
    SC_LOG_ERROR("iSearch: Source graph not specified");
    utils::AgentUtils::finishAgentWork(&m_memoryCtx, actionNode, false);
    return SC_RESULT_ERROR_INVALID_PARAMS;
  }

  // Collect graphs nodes into vectors
  ScAddrVector sourceNodes;
  ScAddrVector patternNodes;

  m_memoryCtx.ForEachIter3(
    source,
    ScType::EdgeAccessConstPosPerm,
    ScType::NodeConst,
    [&] (ScAddr const & src, ScAddr const & edge, ScAddr const & trg)
    {
      sourceNodes.push_back(trg);
    });

  m_memoryCtx.ForEachIter3(
    pattern,
    ScType::EdgeAccessConstPosPerm,
    ScType::NodeConst,
    [&] (ScAddr const & src, ScAddr const & edge, ScAddr const & trg)
    {
      patternNodes.push_back(trg);
    });

  SC_LOG_INFO("iSearch: Search started");
  printf("iSearch: Found %ld nodes in source graph\n", sourceNodes.size());
  printf("iSearch: Found %ld nodes in pattern graph\n", patternNodes.size());

  vector<Isomorphism> isomorphisms;
  ScAddrLessFunc scAddrLessFunc;

  // Vector must be sorted for next_permutation to work properly
  std::sort(sourceNodes.begin(), sourceNodes.end(), scAddrLessFunc);

  size_t permutationsCount = 0;

  size_t skippedPermutationsCount = 0;
  ScAddrVector previousPermutation(sourceNodes.size());
  
  do {
    permutationsCount++;
    
    //If first n elements in permutations are same as in previous, skipping
    bool skipPermutation = true;
    for (size_t i = 0; i < patternNodes.size(); i++) {
      if(previousPermutation[i] != sourceNodes[i]) {
        skipPermutation = false;
        break;
      }
    }

    previousPermutation = sourceNodes;
    
    if(skipPermutation) {
      continue;
      skippedPermutationsCount++;
    }

    Isomorphism isomorphism;

    for (size_t i = 0; i < patternNodes.size(); i++)
      isomorphism.emplace(make_pair(patternNodes[i], sourceNodes[i]));

    bool found = true;
    bool nextPermutation = false;

    for (const auto& p1 : patternNodes) {
        for (const auto& p2 : patternNodes) {
            bool adjInPattern = areAdjacent(p1, p2);
            bool adjInSource = areAdjacent(isomorphism.at(p1), isomorphism.at(p2));
            
            if (adjInPattern != adjInSource) {
                found = false;
                nextPermutation = true;
                break;
            }
        }
        if(nextPermutation) break;
    }

    if (found)
      isomorphisms.push_back(isomorphism);
  } while(std::next_permutation(sourceNodes.begin(), sourceNodes.end(), scAddrLessFunc));

  printf(
    "iSearch: Found %ld isomorphisms. Checked %ld permutations (%ld duplicates skipped)\n",
    isomorphisms.size(),
    permutationsCount,
    skippedPermutationsCount
    );
  
  
  // Output answers
  size_t currentIsomorphismNumber = 1;
  for(const auto& isomorphism : isomorphisms) {
    ScAddr isoNode = m_memoryCtx.CreateNode(ScType::NodeConst);
    ScAddr isoNodeLink = m_memoryCtx.CreateEdge(ScType::EdgeDCommonConst, actionNode, isoNode);
    /* ScAddr relationLink = */ m_memoryCtx.CreateEdge(ScType::EdgeAccessConstPosPerm, Keynodes::nrel_search_result, isoNodeLink);

    printf("iSearch: Isomorphism %ld:\n", currentIsomorphismNumber++);

    for(const auto& pair : isomorphism) {
      printf(
        "iSearch: { %s }->{ %s }\n",
        m_memoryCtx.HelperGetSystemIdtf(pair.first).c_str(),
        m_memoryCtx.HelperGetSystemIdtf(pair.second).c_str()
        );

      ScAddr srcToTrgLink = m_memoryCtx.CreateEdge(ScType::EdgeDCommonConst, pair.first, pair.second);
      /* ScAddr isoNodeToLink = */ m_memoryCtx.CreateEdge(ScType::EdgeAccessConstPosPerm, isoNode, srcToTrgLink);
    }
  }

  utils::AgentUtils::finishAgentWork(ms_context.get(), actionNode, answerElements, true);
  SC_LOG_INFO("iSearch: Search finished");
  
  return SC_RESULT_OK;
}

bool iSearchAgent::areAdjacent(const ScAddr& node1, const ScAddr& node2)
{        
  ScIterator3Ptr adjIt = m_memoryCtx.Iterator3(
    node1,
    ScType::EdgeUCommonConst,
    node2
  );

  if(adjIt->Next())
  {
    return true;
  }

  ScIterator3Ptr adjItRev = m_memoryCtx.Iterator3(
    node2,
    ScType::EdgeUCommonConst,
    node1 
  );

  if(adjItRev->Next())
  {
    return true;
  }

  return false;
}

}