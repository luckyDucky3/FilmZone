﻿using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<IBaseResponse<bool>> CreateFeedback(Feedback feedback);
    }
}