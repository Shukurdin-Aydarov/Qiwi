using System.Text.RegularExpressions;

using Qiwi.Collections;

namespace Qiwi
{
    public static class PersonParser
    {
        public static Queue<Person> CreateQueueFromPhones(string[] phones)
        {
            var queue = new Queue<Person>();
            foreach (var phone in phones)
            {
                if (Regex.IsMatch(phone, "^[+][0-9]{8}$"))
                    queue.Enqueue(new Person { Id = queue.Count + 1, Phone = phone });
            }

            return queue;
        }
    }
}
