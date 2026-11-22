using AutoMapper;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class AddEmployeeService : IAddEmployeeService
    {
       private readonly IAddEmployeeRepository _repository;
       private readonly IMapper _mapper;
        public AddEmployeeService(IAddEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<EmployeeDto>> AddEmployeeAsync(List<EmployeeDto> employee)
       {
            try
            {
                var emp = _mapper.Map<List<Employee>>(employee);

                var resultEmp = await _repository.AddEmployeeAsync(emp);

                return _mapper.Map<List<EmployeeDto>>(resultEmp);
            }
            catch (Exception)
            {

                throw;
            }

           

        }

        //implement  update service
    }
}
