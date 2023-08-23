using AutoMapper;
using Register.API.DTOs;
using Register.App.Controllers;
using Register.Application.Interfaces;
using Register.Application.NotificationPattern;
using Register.Application.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Register.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper, INotifier notifier, ILogger<UserController> logger) : base(mapper, notifier, logger)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                var listaPessoas = await _userService.GetAllUsersWithAddress();

                return Ok(Mapper.Map<IEnumerable<UserDTO>>(listaPessoas));
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetByName([FromQuery] string name)
        {
            try
            {
                var listaPessoas = await _userService.GetUserByName(name);

                return Ok(Mapper.Map<IEnumerable<UserDTO>>(listaPessoas));
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetOneUser(Guid id)
        {
            try
            {
                var pessoa = await _userService.GetOneUserById(id);

                return Ok(Mapper.Map<UserDTO>(pessoa));
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddNewUser([FromBody] UserDTO pessoaDTO)
        {
            try
            {
                var pessoa = Mapper.Map<User>(pessoaDTO);
                await _userService.AddUser(pessoa);

                if (!IsValidOperation()) 
                    return BadRequest(_notifier.GetNotifications());

                return Ok(Mapper.Map<UserDTO>(pessoa));
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDTO pessoaDTO)
        {
            try
            {
                var pessoa = Mapper.Map<User>(pessoaDTO);
                await _userService.UpdateUser(pessoa);

                if (!IsValidOperation())
                    return BadRequest(_notifier.GetNotifications());

                return Ok(Mapper.Map<UserDTO>(pessoa));
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            try
            {
                await _userService.DeleteUser(id);

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
