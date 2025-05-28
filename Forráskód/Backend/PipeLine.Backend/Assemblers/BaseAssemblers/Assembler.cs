namespace PipeLine.Backend.Assemblers.BaseAssemblers
{
    public abstract class Assembler<Tmodel, TDto> : IAssembler<Tmodel, TDto>
        where Tmodel : class, new()
        where TDto : class, new()
    {
        public abstract Tmodel ToModel(TDto dto);
        public abstract TDto ToDto(Tmodel model);

        public virtual List<Tmodel> ToModels(List<TDto> dtos)
        {
            return dtos.Select(dto => ToModel(dto)).ToList();
        }
        public virtual List<TDto> ToDtos(List<Tmodel> models)
        {
            return models.Select(model => ToDto(model)).ToList();
        }
    }
}
