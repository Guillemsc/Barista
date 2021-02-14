using Barista.Shared.Entities.Environment;
using System.Collections.Generic;

namespace Barista.Shared.Dto.Entities
{
    [System.Serializable]
    public class EnvironmentEntityDto
    {
        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }

        public static EnvironmentEntityDto ToDto(EnvironmentEntity heroEntity)
        {
            return new EnvironmentEntityDto()
            {
                TypeId = heroEntity.TypeId,
                InstanceId = heroEntity.InstanceId,
            };
        }

        public static List<EnvironmentEntityDto> ToDto(List<EnvironmentEntity> list)
        {
            List<EnvironmentEntityDto> ret = new List<EnvironmentEntityDto>();

            foreach (EnvironmentEntity entity in list)
            {
                ret.Add(ToDto(entity));
            }

            return ret;
        }
    }
}
