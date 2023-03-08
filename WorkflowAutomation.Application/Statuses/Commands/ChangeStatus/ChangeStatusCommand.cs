using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Statuses.Commands.ChangeStatus
{
    public class ChangeStatusCommand : IRequest
    {
        /// <summary>
        /// ID документа, чей статус изменен
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// ID нового статуса
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// ID пользователя, изменяющего статус документа
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Uri приложения
        /// </summary>
        public Uri? Uri { get; set;}
        /// <summary>
        /// JWT токен для отправки уведомлений
        /// </summary>
        public string? JwtToken { get; set; }
    }
}
