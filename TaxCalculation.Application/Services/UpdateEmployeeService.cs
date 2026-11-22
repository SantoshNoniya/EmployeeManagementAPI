using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Domain.Entities;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class UpdateEmployeeService: IUpdateEmployeeService
    {       
        private readonly IUpdateEmployeeRepository _employeeRepository;
        private readonly IGetEmployeeRepository _getEmployeeRepository;
        private readonly IMapper _mapper;   
        public UpdateEmployeeService(IUpdateEmployeeRepository employeeRepository,IGetEmployeeRepository getEmployeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _getEmployeeRepository = getEmployeeRepository;
            _mapper = mapper;
        }
        public async Task<List<EmployeeDto>> UpdateEmployeeAsync(List<EmployeeDto> employees)
        {
            try
            {
                var updatedEmployees = new List<Employee>();

                foreach (var employeeDto in employees)
                {
                    var existingEmployee =
                        (await _getEmployeeRepository.employeesDetailsAsync())
                        .FirstOrDefault(e => e.Id == employeeDto.Id);

                    if (existingEmployee == null)
                        continue;

                    _mapper.Map(employeeDto, existingEmployee);

                    updatedEmployees.Add(existingEmployee);
                }

                await _employeeRepository.UpdateEmployeeAsync(updatedEmployees);

                return _mapper.Map<List<EmployeeDto>>(updatedEmployees);
            }
            catch (Exception ex)
            {

                throw ;
            }

        }

    }
}
