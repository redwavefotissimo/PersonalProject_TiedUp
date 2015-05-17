using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace tiedup
{
    class dictionary
    {
        private SQLiteConnection sqlcon; // call sqlite connection
        private SQLiteCommand sqlcmd; // call sqlite command
        private SQLiteDataReader sqlrdr; // call sqlite reader
        public string message; // string message
        public string[] words; // list of words
        private int[] word_id; // list of word id's

        private void dbconnect() // connect to the database
        {
            sqlcon = new SQLiteConnection("Data Source=dictionary.s3db");
        }

        private string executequerry(int randnum) // execute the querry
        {
            string word=null;
            try
            {
                sqlcon.Open(); // open the database
                sqlcmd = new SQLiteCommand(sqlcon); // create command
                sqlcmd.CommandText = // sql command
                "SELECT word FROM term_list WHERE word_id=@randnum"; // pass the command to sqlcmd

                // pass the randnum to @randnum for the command
                SQLiteParameter parword = sqlcmd.CreateParameter(); // create parameter
                parword.ParameterName = "@randnum"; // indicate to whom the value will pass on
                parword.Value = randnum; // indicate where to get the value
                sqlcmd.Parameters.Add(parword); // add the value to the specified param name

                sqlrdr = sqlcmd.ExecuteReader(); // create reader

                if (sqlrdr.Read()) // if successfully reads the database
                {
                    word = sqlrdr.GetString(sqlrdr.GetOrdinal("word")).ToString(); // get the word
                }
                else
                {
                    message = "cannot connect to database"; // if error pass the wrning to message
                }

                sqlrdr.Close(); // close the reader
            }
            catch (Exception e)
            {
                message = e.Message; // if error pass the wrning to message
            }
            finally
            {
                if (message == null || message == "")
                {
                    message = "success!"; // if no error pass "success" to message
                }
                sqlcon.Close(); // close the database
            }

            return word; // return the word to method
        }

        public void getword(string level, string kind, int number) // method to get word
        {
            int count = 0; // sets the counter to count the number of loops
            int rownum = 0; // number of rows
            int randnum = 0; // randomed number
            Random rand = new Random(); // create random
            words = new string[number]; // sets the array size
            
            dbconnect(); // connects to the database

            #region get number of row and place word_id's to array
            try
            {
                // get number of row
                DataTable dt = new DataTable(); // create datatable
                sqlcon.Open(); // open the database
                sqlcmd = new SQLiteCommand(sqlcon); // create command
                sqlcmd.CommandText = // sql command
                "select a.*, b.kind as kind from term_list a, term_kind b where a.word=b.word and a.level = case when @level='any' then a.level else @level end and b.kind=@kind group by a.word, b.kind";
                
                //pass the value to level
                SQLiteParameter lvl = sqlcmd.CreateParameter(); // create parameter
                lvl.ParameterName = "@level"; // indicate to whom the value will pass on
                lvl.Value = level; // indicate where to get the value
                sqlcmd.Parameters.Add(lvl); // add the value to the specified param name

                //pass the value to kind
                SQLiteParameter knd = sqlcmd.CreateParameter();
                knd.ParameterName = "@kind";
                knd.Value = kind;
                sqlcmd.Parameters.Add(knd);

                sqlrdr = sqlcmd.ExecuteReader(); // create reader
                dt.Load(sqlrdr); // load all selected datas to dt
                rownum = dt.Rows.Count; // count the number of rows present in dt
                sqlrdr.Close(); // close the reader
                sqlcon.Close(); // close the database

                // get the word_id's
                word_id = new int[rownum]; // sets the array size
                sqlcon.Open(); // open the database
                sqlcmd = new SQLiteCommand(sqlcon); // create command
                sqlcmd.CommandText = // sql command
                "select a.*, b.kind as kind from term_list a, term_kind b where a.word=b.word and a.level = case when @level='any' then a.level else @level end and b.kind=@kind group by a.word, b.kind";

                //pass the value to level
                lvl = sqlcmd.CreateParameter(); // create parameter
                lvl.ParameterName = "@level"; // indicate to whom the value will pass on
                lvl.Value = level; // indicate where to get the value
                sqlcmd.Parameters.Add(lvl); // add the value to the specified param name

                //pass the value to kind
                knd = sqlcmd.CreateParameter();
                knd.ParameterName = "@kind";
                knd.Value = kind;
                sqlcmd.Parameters.Add(knd);

                sqlrdr = sqlcmd.ExecuteReader(); // create reader

                count = 0; // set the counter to 0

                while (count != rownum) // loop until equals to number needed
                {
                    if (sqlrdr.Read())
                    {
                        word_id[count] = sqlrdr.GetInt32(sqlrdr.GetOrdinal("word_id"));
                    }
                    count++;
                }

                sqlrdr.Close(); // close the reader
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            sqlcon.Close(); // close the database
            #endregion

            count = 0; // set the counter to 0

            while (count != number) // loop until equals to number needed 
            {
                randnum = rand.Next(0, rownum); // pass the randomed number
                words[count] = executequerry(word_id[randnum]); //pass it to words[#]
                count++; // increment count
            }
        }
    }
}
