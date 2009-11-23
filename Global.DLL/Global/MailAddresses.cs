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
        /// Adds a new querystring parameter to the Parameters collection.
        ///</summary>
        ///<param name="mail">The specified sender mail.</param>
        ///<returns>The same instance of the mail address class.</returns>
        public MailAddresses Add(string mail)
        {
            AddressCollection.Add(new MailAddress(mail));
            return this;
        }
        ///<summary>
        /// Adds a new querystring parameter to the Parameters collection.
        ///</summary>
        ///<param name="displayName">The name of the sender.</param>
        ///<param name="mail">The specified sender mail.</param>
        ///<returns>The same instance of the mail address class.</returns>
        public MailAddresses Add(string displayName, string mail)
        {
            AddressCollection.Add(new MailAddress(mail, displayName));
            return this;
        }
    }
}