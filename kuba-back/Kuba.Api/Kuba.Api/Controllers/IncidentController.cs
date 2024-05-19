using AutoMapper;
using Kuba.Api.Dtos.Incident;
using Kuba.Api.Errors;
using Kuba.Domain.Interfaces;
using Kuba.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kuba.Api.Controllers
{
    public class IncidentController : ApiBaseController
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMapper _mapper;

        public IncidentController(IIncidentRepository incidentRepository, IMapper mapper)
        {
            _incidentRepository = incidentRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentCreateRequest incidentCreateRequest)
        {
            var incident = _mapper.Map<Incident>(incidentCreateRequest);
            await _incidentRepository.AddAsync(incident);
            return Ok();
        }

        [Authorize(Roles = "Supervisor, Adm")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<IncidentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<IncidentResponse>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<IncidentResponse>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<IncidentResponse>>>> ListIncidents()

        {
            ApiResponse<IEnumerable<IncidentResponse>> apiResponse;

            var incidents = await _incidentRepository.GetAllAsync();

            var incidentsMapped = incidents.Select(_mapper.Map<IncidentResponse>);

            apiResponse = new(StatusCodes.Status200OK, incidentsMapped);

            return Ok(apiResponse);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("byUser/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<IncidentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<IncidentResponse>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<IncidentResponse>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<IncidentResponse>>>> ListIncidentsByUserId
            (
                [FromRoute] Guid id
            )
        {
            var incidents = await _incidentRepository.GetIncidentsByUserId(id);
            
            var incidentsMapped = incidents.Select(_mapper.Map<IncidentResponse>);

            ApiResponse<IEnumerable<IncidentResponse>> apiResponse = 
                new(StatusCodes.Status200OK, incidentsMapped);

            return Ok(apiResponse);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse<IncidentResponse>>> GetIncident([FromRoute] Guid id)
        {
            ApiResponse<IncidentResponse> apiResponse;

            var incident = await _incidentRepository.GetByIdAsync(id);

            if (incident == null)
            {
                apiResponse = new(StatusCodes.Status404NotFound, $"Incident with id: {id} was not found.");
                return NotFound(apiResponse);
            }

            var incidentMapped = _mapper.Map<IncidentResponse>(incident);


            apiResponse = new(StatusCodes.Status200OK, incidentMapped);
            return Ok(apiResponse);
        }

        [Authorize(Roles = "Supervisor, Adm")]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IncidentResponse>>> UpdateIncident(Guid id, [FromBody] IncidentUpdateRequest incidentUpdateRequest)
        {
            ApiResponse<IncidentResponse> apiResponse;

            var incidentDb = await _incidentRepository.GetByIdAsync(id);

            if (incidentDb is null)
            {
                apiResponse = new(StatusCodes.Status404NotFound, $"Incident with id: {id} was not found.");
                return NotFound(apiResponse);
            }

            incidentDb.Title = incidentUpdateRequest.Title is null ? incidentDb.Title : incidentUpdateRequest.Title;
            incidentDb.Description = incidentUpdateRequest.Description is null ? incidentDb.Description : incidentUpdateRequest.Description;
            incidentDb.ReportedDate = (DateTime)(incidentUpdateRequest.ReportedDate is null ? incidentDb.ReportedDate : incidentUpdateRequest.ReportedDate);
            incidentDb.UpdatedBy = incidentUpdateRequest.UpdatedBy;
            incidentDb.SeverityType = incidentUpdateRequest.SeverityType ?? incidentDb.SeverityType;
            incidentDb.IncidentType = incidentUpdateRequest.IncidentType ?? incidentDb.IncidentType;

            await _incidentRepository.UpdateAsync(incidentDb);

            var incidentMapped = _mapper.Map<IncidentResponse>(incidentDb);

            apiResponse = new(StatusCodes.Status200OK, incidentMapped);

            return Ok(apiResponse);
        }


        [Authorize(Roles = "Adm")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<IncidentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IncidentResponse>>> DeleteIncident(Guid id)
        {

            ApiResponse<IncidentResponse> apiResponse;

            var incidentDb = await _incidentRepository.GetByIdAsync(id);

            if (incidentDb == null)
            {
                apiResponse = new(StatusCodes.Status404NotFound, $"Incident with id: {id} was not found.");
                return NotFound(apiResponse);
            }

            await _incidentRepository.RemoveAsync(id);
            apiResponse = new(StatusCodes.Status200OK, $"Resource with id: {id} was deleted successfully");

            return Ok(apiResponse);
        }
    }
}
