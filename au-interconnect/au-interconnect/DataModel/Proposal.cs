using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace AUInterconnect.DataModel
{
    public class Proposal
    {
        public int Id { get; private set; }
        public DateTime CreateTime;
        public int CreatorId;
        public string Title;
        public string Content;
        public bool Approved;
        public bool Declined;


        private Proposal(int id, DateTime createTime, int creatorId, string title,
            string content, bool approved, bool declined)
        {
            Id = id;
            CreateTime = createTime;
            CreatorId = creatorId;
            Title = title;
            Content = content;
            Approved = approved;
            Declined = declined;
        }

        public void Save()
        {
            string queryStr = "UPDATE Proposals SET createTime=@time, creatorId=@creator " +
                "title=@title, content=@content, approved=@app, declined=@dec " +
                "WHERE proposalId=@proposalId";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("proposalId", Id));
                command.Parameters.Add(new SqlParameter("time", CreateTime));
                command.Parameters.Add(new SqlParameter("creator", CreatorId));
                command.Parameters.Add(new SqlParameter("title", Title));
                command.Parameters.Add(new SqlParameter("content", Content));
                command.Parameters.Add(new SqlParameter("app", Approved));
                command.Parameters.Add(new SqlParameter("dec", Declined));
                con.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get the number of proposals that are pending for review.
        /// </summary>
        /// <remarks>
        /// Pending proposals are not accepted and not declined.
        /// </remarks>
        public static int GetPendingProposalCount()
        {
            string queryStr = "SELECT COUNT(*) FROM Proposals WHERE approved=0 AND declined=0";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                con.Open();
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Gets the HTML content of a proposal.
        /// </summary>
        /// <param name="proposalId">Proposal ID</param>
        /// <returns>null if proposal does not exist</returns>
        public static string GetContent(int proposalId)
        {
            string queryStr = "SELECT content FROM Proposals WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                con.Open();
                object obj = command.ExecuteScalar();
                if (obj == DBNull.Value)
                    return null;
                return (string)obj;
            }
        }

        /// <summary>
        /// Updates the HTML content of a proposal.
        /// </summary>
        /// <param name="proposalId">Proposal ID.</param>
        /// <param name="content">HTML content</param>
        /// <returns>true if update successful; false if the number of rows updated
        /// is not 1.</returns>
        /// <exception cref="ArgumentException">content is null or empty.</exception>
        public static bool UpdateContent(int proposalId, string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Proposal content is null or empty.");

            string queryStr = "UPDATE Proposals SET content=@content " +
                "WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                command.Parameters.Add(new SqlParameter("content", content));
                con.Open();
                if (command.ExecuteNonQuery() == 1)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Gets an instance of Proposal from the database.
        /// </summary>
        /// <param name="proposalId">Proposal ID</param>
        /// <returns>null if proposal does not exist.</returns>
        public static Proposal GetProposal(int proposalId)
        {
            string queryStr = "SELECT * FROM Proposals WHERE proposalId=@id";
            using (SqlConnection con = new SqlConnection(Config.SqlConStr))
            {
                SqlCommand command = new SqlCommand(queryStr, con);
                command.Parameters.Add(new SqlParameter("id", proposalId));
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Proposal(
                        (int)reader["proposalId"],
                        (DateTime)reader["createTime"],
                        (int)reader["creatorId"],
                        (string)reader["title"],
                        (string)reader["content"],
                        (bool)reader["approved"],
                        (bool)reader["declined"]);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}