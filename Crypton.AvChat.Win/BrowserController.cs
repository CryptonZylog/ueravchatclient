using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using Crypton.AvChat.Win.ChatModel;
using mshtml;

namespace Crypton.AvChat.Win
{
    class BrowserController
    {
        private WebBrowser _browser;
        private bool _isReady = false;

        private ChatDocument documentTextSource = null;

        // referenced elements
        HTMLDocument _document = null;
        HTMLHtmlElement _html = null;
        HTMLUListElement _messageList = null;
        HTMLBody _body = null;

        HTMLHtmlElement _topicDiv = null;

        public Action OnBrowserRightClick;

        public Action OnDocumentLoadCompleted;

        public BrowserController(WebBrowser browser, ChatDocument document)
        {
            this._browser = browser;
            this.documentTextSource = document;
            this.initializeBrowser();
        }

        public string WindowHtml
        {
            get
            {
                return this._body.innerHTML;
            }
        }

        private void initializeBrowser()
        {
            this._browser.LoadCompleted += _browser_LoadCompleted;
            this._browser.NavigateToString("<!DOCTYPE html><html><body id='body'></body></html>");
        }

        private void _browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this._document = (HTMLDocument)this._browser.Document;
            this._body = (HTMLBody)this._document.getElementById("body");
            this._html = (HTMLHtmlElement)this._document.getElementsByTagName("HTML").OfType<HTMLHtmlElement>().FirstOrDefault();
            this._isReady = true;
            this.buildDocument();
            // pull data from the chat document now
            this.pullDocumentText();
            this.documentTextSource.Nodes.OnChatNodeAdd += onChatDocumentNodeAdd;
            this.documentTextSource.OnTopicChanged += documentTextSource_OnTopicChanged;
            if (this.OnDocumentLoadCompleted != null)
            {
                this.OnDocumentLoadCompleted();
            }
        }

        private void documentTextSource_OnTopicChanged(ChatDocument document)
        {
            this._browser.Dispatcher.Invoke((Action)delegate
            {
                this._topicDiv.innerHTML = document.Topic;
                this.attachUrlOpenHandlers(this._topicDiv.childNodes);
            });
        }

        private void pullDocumentText()
        {
            this._topicDiv.innerHTML = documentTextSource.Topic;
            this.attachUrlOpenHandlers(this._topicDiv.childNodes);
            foreach (var node in this.documentTextSource.Nodes)
            {
                this.RenderChatNode(node);
            }
        }

        private void RenderChatNode(ChatNode node)
        {
            bool isScrollAtBottom = IsVScrollAtBottom;
            HTMLLIElement liNode = (HTMLLIElement)this._document.createElement("LI");
            node.Render(liNode);
            this._messageList.appendChild((IHTMLDOMNode)liNode);
            string innerText = liNode.innerText;
            this.attachUrlOpenHandlers(liNode.childNodes);
            if (isScrollAtBottom)
            {
                IsVScrollAtBottom = true;
            }
        }

        private void attachUrlOpenHandlers(dynamic childNodes)
        {
            // prevent opening of IE and instead send url call to system
            try
            {
                foreach (IHTMLDOMNode domNode in childNodes)
                {
                    HTMLHtmlElement element = null;
                    try
                    {
                        element = (HTMLHtmlElement)domNode;
                    }
                    catch { }
                    if (element != null && element.tagName == "A")
                    {
                        HTMLAnchorElement anchor = (HTMLAnchorElement)element;
                        HTMLAnchorEvents_Event anchorEvents = (HTMLAnchorEvents_Event)element;
                        anchorEvents.onclick += delegate
                        {
                            if (!string.IsNullOrEmpty(anchor.href))
                            {
                                Win32.Win32Impl.Open(anchor.href);
                            }
                            return false;
                        };
                    }

                    this.attachUrlOpenHandlers(domNode.childNodes);
                }
            }
            catch { }
        }

        private void waitForDocumentLoad()
        {
            while (!_isReady)
            {
                DispatcherFrame frame = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate(object f)
                {
                    ((DispatcherFrame)f).Continue = true;
                    return null;
                }), frame);
                Dispatcher.PushFrame(frame);
            }
        }

        private void buildDocument()
        {
            this.buildBaseCSS();
            this.buildThemeCSS();

            HTMLDocumentEvents_Event document = (HTMLDocumentEvents_Event)this._document;
            document.oncontextmenu += delegate
            {
                _browser.ContextMenu.IsOpen = true;
                return false;
            };


            // this will hold the topic text
            this._topicDiv = (HTMLHtmlElement)this._document.createElement("DIV");
            this._topicDiv.setAttribute("id", "topic");
            this._body.appendChild((IHTMLDOMNode)this._topicDiv);

            // this will hold list of messages
            this._messageList = (HTMLUListElement)this._document.createElement("UL");
            this._messageList.id = "messageList";
            this._body.appendChild((IHTMLDOMNode)_messageList);
        }

        private void buildBaseCSS()
        {
            HTMLStyleElement baseCss = (HTMLStyleElement)this._document.getElementById("base_css");
            if (baseCss == null)
            {
                baseCss = (HTMLStyleElement)this._document.createElement("STYLE");
                baseCss.setAttribute("type", "text/css");
                baseCss.setAttribute("id", "base_css");
                this._body.appendChild((IHTMLDOMNode)baseCss);
            }
            baseCss.styleSheet.cssText = Properties.Resources.ChatBaseCss;
        }

        private void buildThemeCSS()
        {
            HTMLStyleElement themeCss = (HTMLStyleElement)this._document.getElementById("theme_css");
            if (themeCss == null)
            {
                themeCss = (HTMLStyleElement)this._document.createElement("STYLE");
                themeCss.setAttribute("type", "text/css");
                themeCss.setAttribute("id", "theme_css");
                this._body.appendChild((IHTMLDOMNode)themeCss);
            }
            string themeCssContent = App.Current.CurrentTheme.GetCss();
            if (themeCssContent != null)
            {
                themeCss.styleSheet.cssText = themeCssContent;
            }
            else
            {
                themeCss.styleSheet.cssText = "/* Theme css not loaded */";
            }
        }

        /// <summary>
        /// Gets the topic text without HTML markup
        /// </summary>
        public string TopicText
        {
            get
            {
                if (this._topicDiv != null)
                {
                    return this._topicDiv.innerText;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets value indicating that the V Scroll bar is at the bottom
        /// </summary>
        public bool IsVScrollAtBottom
        {
            get
            {
                if (this._html != null)
                {
                    int offset = _html.scrollHeight - _html.clientHeight;
                    if (offset < 0)
                        return true;
                    return offset == _html.scrollTop;
                }
                return false;
            }
            set
            {
                if (this._html != null)
                {
                    int offset = this._html.scrollHeight - this._html.clientHeight;
                    if (offset > 0)
                        this._html.scrollTop = offset;
                }
            }
        }

        private void onChatDocumentNodeAdd(ChatNode node)
        {
            this._browser.Dispatcher.Invoke(new Action<ChatNode>(this.RenderChatNode), node);
        }


        public static string ReplaceCorrectHtml(string html)
        {
            return html.Replace("&apos;", "&#39;").Replace("&#x9;", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        }

    }
}
