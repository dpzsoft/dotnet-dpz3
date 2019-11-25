using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo {
    public class People : dpz3.Serializable.Object {

        /// <summary>
        /// 名称
        /// </summary>
        public dpz3.Serializable.UTF8String Name { get; private set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public dpz3.Serializable.Int16 Age { get; private set; }

        /// <summary>
        /// 性别
        /// </summary>
        public dpz3.Serializable.UTF8String Sex { get; private set; }

        public People() {
            this.Name = new dpz3.Serializable.UTF8String();
            this.Reg(this.Name);
            this.Age = new dpz3.Serializable.Int16();
            this.Reg(this.Age);
            this.Sex = new dpz3.Serializable.UTF8String();
            this.Reg(this.Sex);
        }

    }
}
