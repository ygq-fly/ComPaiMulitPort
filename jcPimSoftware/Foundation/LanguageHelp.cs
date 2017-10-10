using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace jcPimSoftware
{
    internal enum ItemLevel { itmLevelA, itmLevelB, itmLevelC};

    internal class ItemNode 
    {
        public string id;
        public string label;

        public ItemNode()
        {
            id = "";
            label = "";
        }
    }

    internal static class LanguageHelp
    {
        private static XmlDocument xDoc;

        private static bool preprocessed;
        internal static bool IsPreprocessID
        {
            get { return preprocessed; }
            set { preprocessed = value; }
        }

        static LanguageHelp()
        {
            preprocessed = true;
        }

        /// <summary>
        /// 加载语言包XML文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        internal static bool Load(string filepath)
        {
            if (xDoc == null)
                xDoc = new XmlDocument();

            try 
            {
                xDoc.Load(filepath);

                return true;

            } catch (XmlException) {
                return false;              
            } 
        }


        #region 获取一个字符串///////////////////////////////////////////////
        
        /// <summary>
        /// 获取一级菜单项，一个
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetMenuItem(string menuid, 
                                    string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelA, "menu", menuid, null, null, notFoundValue);
        }

        /// <summary>
        /// 获取一级菜单项关联的二级菜单项，一个
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="submenuid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetSubmenuItem(string menuid, 
                                       string submenuid, 
                                       string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelB, "menu", menuid, submenuid, null, notFoundValue);
        }

        /// <summary>
        /// 获取二级菜单项关联的三级菜单项，一个
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="submenuid"></param>
        /// <param name="ssubmenuid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetSsubmenuItem(string menuid,
                                        string submenuid, 
                                        string ssubmenuid, 
                                        string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelC, "menu", menuid, submenuid, ssubmenuid, notFoundValue);           
        }

        /// <summary>
        /// 获取一级窗体项，一个
        /// </summary>
        /// <param name="dlgid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetDlgItem(string dlgid,
                                   string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelA, "dialog", dlgid, null, null, notFoundValue); 
        }

        /// <summary>
        /// 获取一级窗体项关联的二级窗体项，一个
        /// </summary>
        /// <param name="dlgid"></param>
        /// <param name="subdlgid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetSubdlgItem(string dlgid,
                                      string subdlgid,
                                      string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelB, "dialog", dlgid, subdlgid, null, notFoundValue);           
        }

        /// <summary>
        /// 获取二级窗体项关联的三级窗体项，一个
        /// </summary>
        /// <param name="dlgid"></param>
        /// <param name="subdlgid"></param>
        /// <param name="ssublgid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetSsubdlgItem(string dlgid,
                                       string subdlgid,
                                       string ssublgid,
                                       string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelC, "dialog", dlgid, subdlgid, ssublgid, notFoundValue);           
        }

        /// <summary>
        /// 获取一级提示字符串项，一个
        /// </summary>
        /// <param name="strid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetStrItem(string strid,
                                   string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelA, "string", strid, null, null, notFoundValue);
        }

        /// <summary>
        /// 获取一级提示字符串项关联的二级提示字符串项，一个
        /// </summary>
        /// <param name="strid"></param>
        /// <param name="substrid"></param>
        /// <param name="notFoundValue"></param>
        /// <returns></returns>
        internal static string GetSubstrItem(string strid,
                                      string substrid,
                                      string notFoundValue)
        {
            return GetItemLabel(ItemLevel.itmLevelB, "string", strid, substrid, null, notFoundValue);           
        }   
     
        #endregion ////////////////////////////////////////////////////////////


        #region 获取一组字符串对/////////////////////////////////////////////

        /// <summary>
        /// 获取一级菜单，一组
        /// </summary>
        /// <returns></returns>
        internal static ItemNode[] GetMenuItems()
        {
            return GetItemsLabel(ItemLevel.itmLevelA, "menu", null, null);
        }

        /// <summary>
        /// 获取一级菜单关联的二级菜单，一组
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        internal static ItemNode[] GetSubmenuItems(string menuid)
        {
            return GetItemsLabel(ItemLevel.itmLevelB, "menu", menuid, null);
        }

        /// <summary>
        /// 获取二级菜单关联的三级菜单，一组
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="submenuid"></param>
        /// <returns></returns>
        internal static ItemNode[] GetSsubmenuItems(string menuid, string submenuid)
        {
            return GetItemsLabel(ItemLevel.itmLevelC, "menu", menuid, submenuid);
        }

        /// <summary>
        /// 获取一级窗体项，一组
        /// </summary>
        /// <returns></returns>
        internal static ItemNode[] GetDlgItems()
        {
            return GetItemsLabel(ItemLevel.itmLevelA, "dialog", null, null);
        }

        /// <summary>
        /// 获取一级窗体项关联的二级窗体项，一组
        /// </summary>
        /// <param name="dlgid"></param>
        /// <returns></returns>
        internal static ItemNode[] GetSubdlgItems(string dlgid)
        {
            return GetItemsLabel(ItemLevel.itmLevelB, "dialog", dlgid, null);
        }

        /// <summary>
        /// 获取二级窗体项关联的三级窗体项，一组
        /// </summary>
        /// <param name="dlgid"></param>
        /// <param name="subdlgid"></param>
        /// <returns></returns>
        internal static ItemNode[] GetSsubdlgItems(string dlgid, string subdlgid)
        {
            return GetItemsLabel(ItemLevel.itmLevelC, "dialog", dlgid, subdlgid);
        }

        /// <summary>
        /// 获取一级提示字符串项，一组
        /// </summary>
        /// <returns></returns>
        internal static ItemNode[] GetStrItems()
        {
            return GetItemsLabel(ItemLevel.itmLevelA, "string", null, null);
        }

        /// <summary>
        /// 获取一级提示字符串项关联的二级提示字符串项，一组
        /// </summary>
        /// <param name="strid"></param>
        /// <returns></returns>
        internal static ItemNode[] GetSubstrItems(string strid)
        {
            return GetItemsLabel(ItemLevel.itmLevelB, "string", strid, null);
        }

        #endregion ////////////////////////////////////////////////////////////


        #region 获取一个文本串 //////////////////////////////////////////////

        private static string GetItemLabel(ItemLevel il,
                                    string catalog,
                                    string id,
                                    string subid,
                                    string ssubid,
                                    string notFoundValue)
        {
            string sResult = "";

            string sId, sSubId, sSsubId;
            if (preprocessed) {
                sId = id + "0000";
                sSubId = id + subid + "00";
                sSsubId = id + subid + ssubid;

            } else {
                sId = id ;
                sSubId = subid;
                sSsubId = ssubid;
            }

            XmlNodeList nodes = xDoc.GetElementsByTagName(catalog);

            if (il == ItemLevel.itmLevelA)
                sResult = GetLabelByID(nodes, sId);

            else if (il == ItemLevel.itmLevelB)
                sResult = GetLabelByID(nodes, sId, sSubId);

            else
                sResult = GetLabelByID(nodes, sId, sSubId, sSsubId);

            if (String.IsNullOrEmpty(sResult))
                return notFoundValue;
            else
                return sResult;
        }

        /// <summary>
        /// 获取一级文本，一次一个
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string GetLabelByID(XmlNodeList nodes, string id)
        {
            XmlNode node = GetNodeByID(nodes, id);

            return GetLabelByNode(node);
        }

        /// <summary>
        /// 获取二级文本，一次一个
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="id"></param>
        /// <param name="subid"></param>
        /// <returns></returns>
        private static string GetLabelByID(XmlNodeList nodes,
                                    string id,
                                    string subid)
        {
            XmlNode node = GetNodeByID(nodes, id);
            if (node == null)
                return "";

            XmlNode subnode = GetNodeByID(node.ChildNodes, subid);

            return GetLabelByNode(subnode);
        }

        /// <summary>
        /// 获取三级文本，一次一个
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="id"></param>
        /// <param name="subid"></param>
        /// <param name="ssubid"></param>
        /// <returns></returns>
        private static string GetLabelByID(XmlNodeList nodes,
                                    string id,
                                    string subid,
                                    string ssubid)
        {
            XmlNode node = GetNodeByID(nodes, id);
            if (node == null)
                return "";

            XmlNode subnode = GetNodeByID(node.ChildNodes, subid);
            if (subnode == null)
                return "";

            XmlNode subsubnode = GetNodeByID(subnode.ChildNodes, ssubid);

            return GetLabelByNode(subsubnode);
        }
        #endregion ////////////////////////////////////////////////////////////

        
        #region 获取一组文本串 /////////////////////////////////////////////
        private static ItemNode[] GetItemsLabel(ItemLevel il,
                                         string catalog,
                                         string id,
                                         string subid)
        {
            string sId, sSubId;
            if (preprocessed) {
                sId = id + "0000";
                sSubId = id + subid + "00";

            }  else {
                sId = id;
                sSubId = subid;
            }

            XmlNodeList nodes = xDoc.GetElementsByTagName(catalog);

            if (il == ItemLevel.itmLevelA)
                return GetLabels(nodes);

            else if (il == ItemLevel.itmLevelB)
                return GetLabelsByID(nodes, sId);

            else
                return GetLabelsByID(nodes, sId, sSubId);
        }

        private static ItemNode[] GetLabels(XmlNodeList nodes)
        {
            return GetLabelsByNodes(nodes);
        }

        private static ItemNode[] GetLabelsByID(XmlNodeList nodes, string id)
        {
            XmlNode node = GetNodeByID(nodes, id);
            if (node == null) 
                return null;

            return GetLabelsByNodes(node.ChildNodes);
        }

        private static ItemNode[] GetLabelsByID(XmlNodeList nodes, string id, string subid)
        {
            XmlNode node = GetNodeByID(nodes, id);
            if (node == null)
                return null;

            XmlNode subnode = GetNodeByID(node.ChildNodes, subid);
            if (subnode == null)
                return null;

            return GetLabelsByNodes(subnode.ChildNodes);
        }
       
        #endregion ////////////////////////////////////////////////////////////
       
        
        #region 从nodes中，获取node, 并获取其对应的文本或文本组///////////

        /// <summary>
        /// 获取nodes文本，一组
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static ItemNode[] GetLabelsByNodes(XmlNodeList nodes)
        {
            int i;
            XmlNode ndID, ndLabel;

            if (nodes == null)
                return null;

            if (nodes.Count <= 0)
                return null;

            ItemNode[] itmNodes = new ItemNode[nodes.Count];
            for (i = 0; i < itmNodes.Length; i++)
                itmNodes[i] = new ItemNode();

            i = 0;
            foreach (XmlNode nd in nodes)
            {
                if (nd.NodeType != XmlNodeType.Element)
                    continue;

                ndID = nd.FirstChild;

                if (ndID == null)
                    continue;

                if (ndID.LocalName != "id")
                    continue;

                ndLabel = ndID.NextSibling;

                if (ndLabel == null)
                    continue;

                if (ndLabel.LocalName != "label")
                    continue;

                itmNodes[i].id    = ndID.InnerText;
                itmNodes[i].label = ndLabel.InnerText;

                i = i + 1;
            }

            return itmNodes;
        }

        /// <summary>
        /// 获取node的文本，一个
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string  GetLabelByNode(XmlNode node)
        {
            string sResult = "";

            if (node == null)
                return "";

            if (node.HasChildNodes)
            {
                XmlNodeList list = node.ChildNodes;

                foreach (XmlNode nd in list)
                {
                    if (nd.NodeType != XmlNodeType.Element)
                        continue;

                    if (nd.LocalName == "label")
                    {
                        sResult = nd.InnerText;
                        break;
                    }
                }
            }

            return sResult;
        }

        /// <summary>
        /// 从nodes中获取id对应的node
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static XmlNode GetNodeByID(XmlNodeList nodes, string id)
        {
            XmlNode nResult = null;
            XmlNode nid;

            if (nodes == null)
                return null;

            if (nodes.Count <= 0)
                return null;

            foreach (XmlNode nd in nodes)
            {
                if (nd.NodeType != XmlNodeType.Element)
                    continue;

                nid = nd.FirstChild;

                if (nid == null)
                    continue;

                if ((nid.LocalName == "id") && (nid.InnerText == id))
                {
                    nResult = nd;
                    break;
                }
            }

            return nResult;
        }
        #endregion ////////////////////////////////////////////////////////////

    }
}