using Dotnet.Sample.Api.Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.Services.User
{
    public interface IContactUsService
    {
        /// <summary>
        /// Get all ContactUsMessage
        /// </summary>
        /// <returns>List of ContactUsMessage entities</returns>
        IList<ContactUsMessage> GetAllMessages();
        IList<ContactUsMessage> GetAllMessages(string email);

        /// <summary>
        /// Get ContactUsMessage using id
        /// </summary>
        /// <param name="id">ContactUsMessage id</param>
        /// <returns>ContactUsMessage entity</returns>
        ContactUsMessage GetMessageById(Guid id);

        /// <summary>
        /// Insert ContactUsMessage
        /// </summary>
        /// <param name="message">ContactUsMessage entity</param>
        Task InsertMessage(ContactUsMessage message);

        /// <summary>
        /// Update ContactUsMessage
        /// </summary>
        /// <param name="message">ContactUsMessage entity</param>
        Task UpdateMessage(ContactUsMessage message);

        /// <summary>
        /// Delete ContactUsMessage
        /// </summary>
        /// <param name="ids">List of ContactUsMessage ids</param>
        Task DeleteMessages(IList<Guid> ids);

        /// <summary>
        /// Mark the ContactUsMessage as read
        /// </summary>
        /// <param name="id">ContactUsMessage id</param>
        Task MarkAsRead(Guid id);
    }
    public class ContactUsService : IContactUsService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public ContactUsService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all ContactUsMessage
        /// </summary>
        /// <returns>List of ContactUsMessage entities</returns>
        public IList<ContactUsMessage> GetAllMessages()
        {
            var entities = _context.ContactUsMessage.OrderByDescending(x => x.SendDate).ToList();

            return entities;
        }

        /// <summary>
        /// Get ContactUsMessage using id
        /// </summary>
        /// <param name="id">ContactUsMessage id</param>
        /// <returns>ContactUsMessage entity</returns>
        public ContactUsMessage GetMessageById(Guid id)
        {
            return _context.ContactUsMessage.Find(id);
        }

        /// <summary>
        /// Insert ContactUsMessage
        /// </summary>
        /// <param name="message">ContactUsMessage entity</param>
        public async Task InsertMessage(ContactUsMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _context.ContactUsMessage.Add(message);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update ContactUsMessage
        /// </summary>
        /// <param name="message">ContactUsMessage entity</param>
        public async Task UpdateMessage(ContactUsMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _context.ContactUsMessage.Update(message);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete ContactUsMessage
        /// </summary>
        /// <param name="ids">List of ContactUsMessage ids</param>
        public async Task DeleteMessages(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.ContactUsMessage.Remove(GetMessageById(id));

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Mark the ContactUsMessage as read
        /// </summary>
        /// <param name="id">ContactUsMessage id</param>
        public async Task MarkAsRead(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var message = GetMessageById(id);
            message.Read = true;

            _context.ContactUsMessage.Update(message);
            await _context.SaveChangesAsync();
        }

        public IList<ContactUsMessage> GetAllMessages(string email)
        {
            return _context.ContactUsMessage.Where(users => users.Email == email).OrderByDescending(x => x.SendDate).ToList();
        }

        #endregion
    }
}
