using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Podcast
{
    public class PodcastResponse
    {
        public Resposta Resposta { get; set; }

        public Podcast Podcast { get; set; }

        public PodcastResponse()
        {
            Resposta = new Resposta();
            Podcast = new Podcast();
        }
    }
}
