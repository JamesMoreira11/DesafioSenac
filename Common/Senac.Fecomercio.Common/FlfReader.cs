using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace Senac.Fecomercio.Common
{
    public class FlfReader : IDisposable
    {
        #region Field Class Mapping

        public class Field
        {
            public string Name { get; set; }

            public string Value { get; set; }

            public int Length { get; set; }

            public int Start { get; set; }
        }

        #endregion

        #region Private variables

        private Stream stream;
        private StreamReader reader;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new reader for the given stream.
        /// </summary>
        /// <param name="s">The stream to read the CSV from.</param>
        public FlfReader(Stream s) : this(s, null) { }

        /// <summary>
        /// Create a new reader for the given stream and encoding.
        /// </summary>
        /// <param name="s">The stream to read the FLF from.</param>
        /// <param name="enc">The encoding used.</param>
        public FlfReader(Stream s, Encoding enc)
        {
            stream = s;
            if (!s.CanRead)
            {
                throw new FLFReaderException("Could not read the given FLF stream!");
            }
            reader = (enc != null) ? new StreamReader(s, enc) : new StreamReader(s);
        }

        /// <summary>
        /// Creates a new reader for the given text file path.
        /// </summary>
        /// <param name="filename">The name of the file to be read.</param>
        public FlfReader(string filename) : this(filename, null) { }

        /// <summary>
        /// Creates a new reader for the given text file path and encoding.
        /// </summary>
        /// <param name="filename">The name of the file to be read.</param>
        /// <param name="enc">The encoding used.</param>
        public FlfReader(string filename, Encoding enc)
            : this(new FileStream(filename, FileMode.Open), enc) { }

        #endregion

        private List<Field> GetFields(string mappingFile)
        {
            List<Field> fields = new List<Field>();
            XmlDocument map = new XmlDocument();

            //Load the mapping file into the XmlDocument
            map.Load(mappingFile);

            //Load the field nodes.
            XmlNodeList fieldNodes = map.SelectNodes("/FileMap/Field");

            //Loop through the nodes and create a field object
            // for each one.
            if (fieldNodes != null) 
                foreach (XmlNode fieldNode in fieldNodes)
                {
                    Field field = new Field
                    {
                        Name = fieldNode.Attributes["Name"].Value,
                        Length = Convert.ToInt32(fieldNode.Attributes["Length"].Value),
                        Start = Convert.ToInt32(fieldNode.Attributes["Start"].Value)
                    };

                    //Set the field's name

                    //Set the field's length

                    //Set the field's starting position

                    //Add the field to the Field list.
                    fields.Add(field);
                }

            //Return the fields – this is now our map of how the flat
            // file is laid out.
            return fields;
        }

        public List<List<Field>> ParseFile(string mappingFile)
        {
            //Get the field mapping.
            List<Field> fields = GetFields(mappingFile);
            //Create a List<List<Field>> collection of collections.
            // The main collection contains our records, and the
            // sub collection contains the fields each one of our
            // records contains.
            List<List<Field>> records = new List<List<Field>>();

            //Load the first line of the file.
            string line = reader.ReadLine();

            //Loop through the file until there are no lines
            // left.
            while (!string.IsNullOrEmpty(line))
            {
                //Se a linha não for vazia, verifico apenas as primeiras posicoes 
                //para certificar mensagem que deve ser "traduzida".
                //Caso 901, erro para todas as mensagens, portanto implementação ficou innner scope.
                if(line.Substring(0, 3).Equals("901"))
                    fields = GetFields(ConfigurationManager.AppSettings["GTeC.Socket.DiretorioMapping"] + "\\901.xml");

                //Create out record (field collection)
                List<Field> record = new List<Field>();

                //Loop through the mapped fields
                foreach (Field field in fields)
                {
                    Field fileField = new Field
                    {
                        Value = line.Substring(field.Start, field.Length),
                        Name = field.Name,
                        Length = field.Length,
                        Start = field.Start
                    };

                    //Use the mapped field's start and length
                    // properties to determine where in the
                    // line to pull our data from.

                    //Set the name of the field.

                    //tests only

                    //Add the field to our record.
                    record.Add(fileField);
                }

                //Add the record to our record collection
                records.Add(record);

                //Read the next line.
                line = reader.ReadLine();
            }

            //Return all of our records.
            return records;
        }

        public void ParseFile(string mappingHeader, out List<List<Field>> headerRecords, string mappingDetail, out List<List<Field>> detailRecords, string mappingFooter, out List<List<Field>> footerRecords)
        {
            headerRecords = new List<List<Field>>();
            detailRecords = new List<List<Field>>();
            footerRecords = new List<List<Field>>();

            string footerLine = null;

            string line = reader.ReadLine();

            #region Header

            if (!string.IsNullOrEmpty(mappingHeader) && !string.IsNullOrEmpty(line))
            {
                var headerLine = line;

                List<Field> headerFields = GetFields(mappingHeader);
                List<Field> headerRecord = new List<Field>();

                foreach (Field field in headerFields)
                {
                    Field fileField = new Field
                    {
                        Value = headerLine.Substring(field.Start, field.Length),
                        Name = field.Name
                    };


                    headerRecord.Add(fileField);
                }

                headerRecords.Add(headerRecord);

                line = reader.ReadLine();
            }

            #endregion

            #region Detail

            if (!string.IsNullOrEmpty(line))
            {
                List<Field> detailFields = GetFields(mappingDetail);

                while (true)
                {
                    var detailLine = line;
                    line = reader.ReadLine();

                    if (line == null)
                    {
                        footerLine = detailLine;
                        break;
                    }

                    if (!string.IsNullOrEmpty(mappingDetail))
                    {
                        List<Field> detailRecord = new List<Field>();

                        foreach (Field field in detailFields)
                        {
                            Field fileField = new Field
                            {
                                Value = detailLine.Substring(field.Start, field.Length),
                                Name = field.Name
                            };


                            detailRecord.Add(fileField);
                        }

                        detailRecords.Add(detailRecord);
                    }
                }
            }

            #endregion

            #region Footer

            if (!string.IsNullOrEmpty(mappingFooter) && !string.IsNullOrEmpty(footerLine))
            {
                List<Field> footerFields = GetFields(mappingFooter);

                List<Field> footerRecord = new List<Field>();

                foreach (Field field in footerFields)
                {
                    Field fileField = new Field
                    {
                        Value = footerLine.Substring(field.Start, field.Length),
                        Name = field.Name
                    };


                    footerRecord.Add(fileField);
                }

                footerRecords.Add(footerRecord);
            }

            #endregion
        }

        public void Dispose()
        {
            // Closing the reader closes the underlying stream, too
            if (reader != null) reader.Close();
            else if (stream != null)
                stream.Close(); // In case we failed before the reader was constructed
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Exception class for FlfReader exceptions.
    /// </summary>
    public class FLFReaderException : ApplicationException
    {
        /// <summary>
        /// Constructs a new exception object with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public FLFReaderException(string message) : base(message) { }
    }
}
