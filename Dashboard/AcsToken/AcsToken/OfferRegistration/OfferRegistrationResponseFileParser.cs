//
// Copyright (c) Microsoft Corporation. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
//
namespace Lomo.Commerce.AmexWorker
{
    using System.IO;
    using System.Collections.ObjectModel;
    using Lomo.Commerce.AmexClient;
    using Lomo.Commerce.DataContracts.Extensions;
    using Lomo.Commerce.Logging;

    /// <summary>
    /// Parses a Amex Offer Registration Response File
    /// </summary>
    public class OfferRegistrationResponseFileParser
    {
        /// <summary>
        /// Create a new instance of the Response File Parser
        /// </summary>
        /// <param name="log">
        /// Commerce Logger
        /// </param>
        public OfferRegistrationResponseFileParser(CommerceLog log)
        {
            Log = log;
        }

        /// <summary>
        /// Parse the Response file
        /// </summary>
        /// <param name="fileName">
        /// Name of the file being parsed
        /// </param>
        /// <param name="stream">
        /// Stream of data capturing the contents
        /// </param>
        /// <returns>
        /// Instance of Response File 
        /// </returns>
        public OfferRegistrationResponseFile Parse(string fileName, Stream stream)
        {
            OfferRegistrationResponseFile responseFile = new OfferRegistrationResponseFile();

            string recordType;
            OfferRegistrationResponseHeader header = null;
            OfferRegistrationResponseTrailer trailer =null;
            Collection<OfferRegistrationResponseDetail> detailRecords = new Collection<OfferRegistrationResponseDetail>();
            if (stream != null)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string headerStr = reader.ReadLine();
                    
                    header = new OfferRegistrationResponseHeader(headerStr);

                    while ((Line = reader.ReadLine()) != null)
                    {
                        recordType = Line.Substring(0, 1);
                        if (recordType == "D")
                        {
                            OfferRegistrationResponseDetail details = new OfferRegistrationResponseDetail(Line);
                            detailRecords.Add(details);
                        }
                        if (recordType == "T")
                        {
                            trailer = new OfferRegistrationResponseTrailer(Line);
                            // should be last line
                            break;
                        }
                    }
                }
            }

            // verify integrity
            if (trailer.TrailerCount != detailRecords.Count)
            {
                Log.Warning("Number of Records suggested by trailer Amex Offer Registration Response file \"{0}\" do not match.",
                             (int)ResultCode.FileMissingExpectedRecord, fileName);
            }

            responseFile.Header = header;
            responseFile.Trailer = trailer;
            foreach (OfferRegistrationResponseDetail detailRecord in detailRecords)
            {
                responseFile.ResponseRecords.Add(detailRecord);
            }

            return responseFile;

        }

        /// <summary>
        /// Gets or sets the line currently being parsed.
        /// </summary>
        private string Line { get; set; }

        /// <summary>
        /// Gets or sets the CommerceLog object through which log entries can be made.
        /// </summary>
        private CommerceLog Log { get; set; }
    }
}