using AutoMapper;
using Register.API.DTOs;
using Register.Application.Interfaces;
using Register.Application.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Core.Controllers;
using Core.NotifierErrors;

namespace Register.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : MainController<UserController>
    {
        private readonly IUserService _userService;
        private readonly IMapper Mapper;

        public UserController(IUserService userService, IMapper mapper, INotifier notifier, ILogger<UserController> logger) : base(notifier, logger)
        {
            _userService = userService;
            Mapper = mapper;
        }

        [HttpGet("[action]")]
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

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            try
            {
                var userId = GetUserId();
                var listaPessoas = await _userService.GetOneUserById(userId);

                return Ok(Mapper.Map<UserDTO>(listaPessoas));
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

        [HttpPut]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var userId = GetUserId();
                var user = Mapper.Map<User>(userDTO);
                user.Id = userId;
                await _userService.UpdateUser(user);

                if (!IsValidOperation())
                    return BadRequest(_notifier.GetNotifications());

                return Ok(Mapper.Map<UserDTO>(user));
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
