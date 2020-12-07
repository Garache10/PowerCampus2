using Aplicacion.Groups;
using Dominio;
using Dominio.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Obtener todos los registros
        public async Task<ActionResult<IEnumerable<T_group>>> GetGroup()
        {
            return await _mediator.Send(new ConsultaGroup.ListaGroups());
        }

        //Obtener por id_group
        [HttpGet("{id_group}")]
        public async Task<ActionResult<T_group>> GetGroup(int id_group)
        {
            var group = await _mediator.Send(new ConsultaIdGroup.OnlyGroup { id_group = id_group });

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        //Obtener grupos por curso
        [HttpGet("GroupByCourseT/{id_course}")]
        public async Task<ActionResult<List<T_group>>> GetGroupByCourse(int id_course)
        {
            var group = await _mediator.Send(new GroupByCourse.GrupoPorCurso { course_id = id_course });
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        //Obtener grupos por curso en vista
        [HttpGet("GroupByCourse/{id_course}")]
        public async Task<ActionResult<List<V_groupsByCourse>>> GetGroupByCourseV(int id_course)
        {
            var group = await _mediator.Send(new GroupsByCourse_v.GrupoPorCursoV { course_id = id_course });
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        [HttpGet("GroupBy/{teacher_id}")]
        public async Task<ActionResult<IEnumerable<V_groupsForTeacher>>> GetGroup(string teacher_id)
        {
            teacher_id = (string)this.RouteData.Values["teacher_id"];
            var group = await _mediator.Send(new GruposPorDocente.GroupsForTeacher { teacher_id = teacher_id });
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        [HttpGet("GroupByT/{teacher_id}")]
        public async Task<ActionResult<IEnumerable<T_group>>> GetGroupT(string teacher_id)
        {
            teacher_id = (string)this.RouteData.Values["teacher_id"];
            var group = await _mediator.Send(new ConsultaGroupsByTeacher.GroupsByTeacher { teacher_id = teacher_id });
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        //Obtener los estudiantes de un grupo pero solo con id 
        [HttpGet("EstudiantesByGroupT/{group_id}")]
        public async Task<ActionResult<IEnumerable<T_det_inscription>>> GetEstudiantesByGroup(int group_id)
        {
            var group = await _mediator.Send(new EstudiantesByGroup.ListEstudiantesByGroup { group_id = group_id });
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        //Obtener los estudiantes de un grupo con vista
        [HttpGet("EstudiantesByGroup/{group_id}")]
        public async Task<ActionResult<IEnumerable<V_estudiantesByGroup>>> GetEstudiantesByGroup_v(int group_id)
        {
            var group = await _mediator.Send(new EstudiantesByGroup_v.estudiantes_v { id_group = group_id });
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        //Insertar un grupo
        [HttpPost]
        public async Task<ActionResult<Unit>> PostGroup(AgregarGroup.newGroup data)
        {
            return await _mediator.Send(data);
        }

        //Modificar un grupo
        [HttpPut("{id_group}")]
        public async Task<ActionResult<Unit>> PutGroup(int id_group, EditarGroup.editGroup data)
        {
            data.id_group = id_group;
            return await _mediator.Send(data);
        }

        //Borrar un grupo
        [HttpDelete("{id_group}")]
        public async Task<ActionResult<Unit>> DeleteGroup(int id_group)
        {
            return await _mediator.Send(new EliminarGroup.deleteGroup { id_group = id_group });
        }

        //Obtener el horario de un grupo
        [HttpGet("horario/{id_group}")]
        public async Task<ActionResult<IEnumerable<T_horario>>> GetHorarioByGroup(int id_group)
        {
            var Horario = await _mediator.Send(new HorarioByGroup.horarioPorGrupo { id_group = id_group });
            if (Horario == null)
            {
                return NotFound();
            }
            return Horario;
        }

        //Agregar horario a un grupo
        [HttpPost("horario")]
        public async Task<ActionResult<Unit>> PostHorario(AgregarHorario.newHorario data)
        {
            return await _mediator.Send(data);
        }

        //Modificar horario de un grupo
        [HttpPut("horario/{id_horario}")]
        public async Task<ActionResult<Unit>> PutHorario(int id_horario, EditarHorario.editHorario data)
        {
            data.id_horario = id_horario;
            return await _mediator.Send(data);
        }

        //Eliminar un horario
        [HttpDelete("horario/{id_horario}")]
        public async Task<ActionResult<Unit>> DeleteHorario(int id_horario)
        {
            return await _mediator.Send(new EliminarHorario.deleteHorario { id_horario = id_horario });
        }
    }
}
