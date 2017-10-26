using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.CustomWizardPages.PipelineComponentWizard
{
    /// <summary>
    /// defines the types of pipeline components we support
    /// see SDK\Include\Pipeline_Int.idl
    /// </summary>
    internal enum componentTypes
    {
      /// <summary>
      /// links to CATID_Decoder
      /// </summary>
      Decoder = 0,
      /// <summary>
      /// links to CATID_DisassemblingParser
      /// </summary>
      DisassemblingParser,
      /// <summary>
      /// links to CATID_Validate
      /// </summary>
      Validate,
      /// <summary>
      /// links to CATID_PartyResolver
      /// </summary>
      PartyResolver,
      /// <summary>
      /// links to CATID_Any
      /// </summary>
      Any,

      /// <summary>
      /// links to CATID_Encoder
      /// </summary>
      Encoder,
      //PreAssembler,	// BUG: Pre-Assembler has no specific CATID associated
      /// <summary>
      /// links to CATID_AssemblingSerializer
      /// </summary>
      AssemblingSerializer,
    }
}
