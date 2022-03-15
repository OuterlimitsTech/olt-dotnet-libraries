////using System;
////using Microsoft.EntityFrameworkCore.Metadata.Builders;
////using OLT.Core;

////using Xunit;

////namespace OLT.Libraries.UnitTest.OLT.EF.Core.EntityTypeConfiguration.Enum
////{  

////    public class BaseTestEnumEntityTypeConfiguration<TEntity, TEnum> : OltEntityTypeConfiguration<TEntity, TEnum>
////        where TEntity : class, IOltEntity, IOltEntityId, new()
////        where TEnum : System.Enum
////    {

////        public virtual void RunMapTests(TEnum @enum, string code, string name, short sortOrder, Guid? uidResult = null)
////        {
////            Assert.NotNull(EnumType);
////            Assert.Equal(typeof(TEnum), EnumType);
////            Assert.True(DefaultSort > 0);

////            var entity = new TEntity();
////            base.Map(entity, @enum);            

////            if (entity is IOltEntityCodeValue codeValueEntity)
////            {
////                Assert.Equal(code, codeValueEntity.Code);
////                Assert.Equal(name, codeValueEntity.Name);
////            }

////            if (entity is IOltEntitySortable sortableEntity)
////            {
////                Assert.Equal(sortOrder, sortableEntity.SortOrder);
////            }

////            if (entity is IOltEntityAudit auditEntity)
////            {
////                Assert.Equal(DefaultUsername, auditEntity.CreateUser);
////                Assert.Equal(DefaultCreateDate, auditEntity.CreateDate);
////            }


////            if (entity is IOltEntityUniqueId uidTableEntity)
////            {
////                var uid = GetUniqueId(@enum);
////                if (uid.HasValue)
////                {
////                    Assert.Equal(uidResult, uid);
////                }
////                else
////                {
////                    Assert.NotEqual(Guid.Empty, uidTableEntity.UniqueId);
////                }
////            }            
////        }

////        public virtual void RunMapTests()
////        {
////            var list = Map();
////            var values = System.Enum.GetValues(EnumType);
////            Assert.Equal(values.Length, list.Count);
////        }

////    }
////}
