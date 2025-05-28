namespace PipeLine.Backend.Assemblers.BaseAssemblers
{
    public interface IAssembler<Tmodel, TDto>
    {
        Tmodel ToModel(TDto dto);
        TDto ToDto(Tmodel model);

        List<Tmodel> ToModels(List<TDto> dtos);
        List<TDto> ToDtos(List<Tmodel> models);
    }
}
