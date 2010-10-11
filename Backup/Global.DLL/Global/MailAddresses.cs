using System.Collections.Generic;
using System.Net.Mail;

namespace Global.Global
{
    ///<summary>
    /// A collection of mail addresses.
    ///</summary>
    public class MailAddresses
    {
        private MailAddressCollection _adressCollection;
        internal MailAddressCollection AddressCollection
        {
            get
            {
                return _adressCollection ?? (_adressCollection = new MailAddressCollection());
            }
        }

        ///<summary>
        /// Adds a new address to the address collection.
        ///</summary>
        ///<param name="mail">The specified sender mail.</param>
        ///<returns>The same instance of the mail address class.</returns>
        public MailAddresses Add(string mail)
        {
            AddressCollection.Add(new MailAddress(mail));
            return this;
        }
        ///<summary>
        /// Adds a new address to the address collection.
        ///</summary>
        ///<param name="displayName">The name of the sender.</param>
        ///<param name="mail">The specified sender mail.</param>
        ///<returns>The same instance of the mail address class.</returns>
        public MailAddresses Add(string displayName, string mail)
        {
            AddressCollection.Add(new MailAddress(mail, displayName));
            return this;
        }

        ///<summary>
        /// Adds a new address to the address collection.
        ///</summary>
        ///<param name="mails">A list of emails.</param>
        ///<returns>The same instance of the mail address class.</returns>
        public MailAddresses Add(IEnumerable<string> mails)
        {
            foreach (var mail in mails)
            {
                AddressCollection.Add(new MailAddress(mail));
            }
            return this;
        }

        ///<summary>
        /// Adds a new address to the address collection.
        ///</summary>
        ///<param name="contacts">A list of contacts.</param>
        ///<returns>The same instance of the mail address class.</returns>
        public MailAddresses Add(Dictionary<string, string> contacts)
        {
            foreach (var contact in contacts)
            {
                AddressCollection.Add(new MailAddress(contact.Value, contact.Key));
            }
            return this;
        }
    }
}