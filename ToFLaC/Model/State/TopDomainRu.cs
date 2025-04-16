using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFLaC.Model.State
{
    public class TopDomainRu : IURLFinderState
    {
        private string _forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?\t\n\r";

        public void Enter(URLFinder urlFinder)
        {
            if (urlFinder.Text[urlFinder.CurrentIdx] == 'u'
                && !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx + 1]))
            {
                if (urlFinder.Text[urlFinder.CurrentIdx + 1] == '/')
                {
                    urlFinder.CurrentIdx++;
                    urlFinder.States.Add("TDRu2");
                    urlFinder.TopDomain = "ru";
                    urlFinder.State = new Path();
                    return;
                }
                else
                {
                    urlFinder.DomainStartIdx = urlFinder.CurrentIdx - 1;
                    urlFinder.CurrentIdx++;
                    urlFinder.States.Add($"D{urlFinder.cntDomain}");
                    urlFinder.cntDomain++;
                    urlFinder.State = new DomainPart();
                    return;
                }
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx] == 'u')
            {
                urlFinder.CurrentIdx++;
                urlFinder.States.Add("TDRu2");
                urlFinder.TopDomain = "ru";
                urlFinder.State = new Path();
                return;
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx] != 'u' &&
                !_forbiddenChars.Contains(urlFinder.Text[urlFinder.CurrentIdx]) &&
                urlFinder.Text[urlFinder.CurrentIdx] != '/')
            {
                urlFinder.DomainStartIdx = urlFinder.CurrentIdx - 1;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.cntDomain++;
                urlFinder.State = new DomainPart();
                return;
            }
            else if (urlFinder.Text[urlFinder.CurrentIdx] == '.')
            {
                urlFinder.DomainCount++;
                urlFinder.CurrentIdx++;
                urlFinder.States.Add($"D{urlFinder.cntDomain}");
                urlFinder.Domain.Add(urlFinder.Text.Substring(urlFinder.CurrentIdx, 1));
                urlFinder.State = new NextDomain();
                return;
            }

            urlFinder.State = new FirstEnter();
            return;
        }
    }
}
